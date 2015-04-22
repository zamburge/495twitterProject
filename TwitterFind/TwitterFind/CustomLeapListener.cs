using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leap;

namespace TwitterFind
{
    public class CustomLeapListener : Listener
    {
        private long _now;
        private long _previous;
        private long _timeDifference;
        public event Action<Vector> OnFingersRegistered;
        public event Action<GestureList> OnGestureMade;

        static void ActivateMouse(Frame frame)
        {
            if (!frame.Fingers.Frontmost.IsValid) return;
            LeapAsMouse.EnableCursorLeap(frame);
        }

        public override void OnConnect(Controller controller)
        {
            controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
            controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
        }


        public override void OnExit(Controller controller) { }

        public override void OnFrame(Controller controller)
        {
            using (var frame = controller.Frame())
            {
                _now = frame.Timestamp;
                _timeDifference = _now - _previous;

                if (frame.Hands.IsEmpty || _timeDifference < 5000) return;
                _previous = frame.Timestamp;

                if (frame.Gestures().Count > 0 && OnGestureMade != null)
                    OnGestureMade(frame.Gestures());

                if (frame.Fingers.Count > 0 && OnFingersRegistered != null)
                {
                    var vector = frame.InteractionBox.NormalizePoint(frame.Fingers.Frontmost.StabilizedTipPosition);
                    OnFingersRegistered(vector);
                }

                ActivateMouse(controller.Frame());
            }

        }

    }
}
