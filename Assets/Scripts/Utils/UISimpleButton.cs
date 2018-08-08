using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utils
{
    public class UISimpleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        protected Action _callback = delegate { };
        protected Action<string> _callbackWithUid = delegate { };
        protected Action<bool, PointerEventData> _press = delegate { };
        protected Action<int> _callbackWithId = delegate { };
        protected string _uid;
        protected int _id;

        public virtual void Listen(Action click) { _callback = click; }

        public virtual void Listen(Action<string> click, string uid)
        {
            _callbackWithUid = click;
            _uid = uid;
        }
		
        public virtual void Listen(Action<int> click, int uid)
        {
            _callbackWithId = click;
            _id = uid;
        }		

        public virtual void Listen(Action click, Action<bool, PointerEventData> press) {
            _callback = click;
            _press = press;
        }

        public virtual void Unlisten() {
            _callback = delegate { };
            _press = delegate { };
        }

        public void OnPointerDown(PointerEventData eventData) { _press(true, eventData); }

        public void OnPointerUp(PointerEventData eventData) { _press(false, null); }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_callback != null) _callback();
            if (_callbackWithUid != null) _callbackWithUid(_uid);
            if (_callbackWithId != null) _callbackWithId(_id);
        }
    }
}