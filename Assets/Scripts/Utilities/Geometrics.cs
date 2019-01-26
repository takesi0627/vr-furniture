using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hekira.Utilities
{
    public class Geometrics
    {
        /// <summary>
        /// Gets the intersection point on sphere whose radius is <paramref name="radius"/> and the is
        /// centering at (0, 0, 0).
        /// <see cref="http://www.geocities.co.jp/SiliconValley-PaloAlto/5227/note/log1.htm#990511"/>
        /// </summary>
        /// <returns>The intersection point on sphere.</returns>
        /// <param name="startPoint">Start point.</param>
        /// <param name="direction">Direction.</param>
        /// <param name="radius">Radius.</param>
        public static Vector3 GetIntersectionPointOnSphere (Vector3 startPoint, Vector3 direction, float radius){
            Vector3 O = Vector3.zero;
            Vector3 PO = O - startPoint;
            Vector3 A = startPoint + Vector3.Project(PO, direction);
            float AO = Vector3.Distance(A, O);
            float effecient = Mathf.Sqrt(radius * radius - AO * AO);
            return A + effecient * direction.normalized;
        }

        public static Vector3 GetIntersectionPointOnSpecifiedYPlane (Vector3 startPoint, Vector3 direction, float radius, float y) {
            startPoint.y = y;
            Vector3 newDirection = Vector3.ProjectOnPlane(direction, Vector3.up);
            return GetIntersectionPointOnSphere(startPoint, newDirection, radius);
        }
    }
}