using UnityEngine;

namespace Helpers {
    //Mostly taken from https://en.wikipedia.org/wiki/B%C3%A9zier_curve
    public class Bezier {
        public static Vector3 QuadraticBezier(Vector3 start, Vector3 control, Vector3 end, float t) {
            float oneMinusT = 1 - t;
            return start * (oneMinusT * oneMinusT) + 2 * control * oneMinusT * t + end * (t * t);
        }

        public static Vector3 QuadraticBezierDerivative(Vector3 start, Vector3 control, Vector3 end, float t) {
            return 2 * (1 - t) * (control - start) + 2 * t * (end - control);
        }

    }
}