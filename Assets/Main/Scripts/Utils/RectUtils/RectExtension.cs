using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main.Scripts.Utils.RectUtils
{
    public static class RectUtils
    {
        // TODO: will be optimized
        public static SlicedRect GetSlicedRect(this Rect rect, Vector2 pointPosition, float rectPieceDirectionAngle)
        {
            Vector2 leftPoint = new Vector2(rect.center.x - rect.width / 2f, rect.center.y);
            Vector2 rightPoint = new Vector2(rect.center.x + rect.width / 2f, rect.center.y);
            Vector2 bottomPoint = new Vector2(rect.center.x, rect.center.y - rect.height / 2f);
            Vector2 upPoint = new Vector2(rect.center.x, rect.center.y + rect.height / 2f);

            List<Vector2> points = new List<Vector2>()
            {
                leftPoint,
                rightPoint,
                bottomPoint,
                upPoint
            };

            Vector2 minDistancePoint = points.OrderBy(x => Vector2.Distance(pointPosition, x)).First();

            Rect firstRect = new Rect();
            Rect secondRect = new Rect();;

            if (minDistancePoint.Equals(leftPoint))
            {
                firstRect = new Rect(0f, 0f, rect.width, rect.height / 2f);
                secondRect = new Rect(0f, rect.height / 2f, rect.width, rect.height / 2f);
            }
            else if (minDistancePoint.Equals(rightPoint))
            {
                firstRect = new Rect(0f, rect.height / 2f, rect.width, rect.height / 2f);
                secondRect = new Rect(0f, 0f, rect.width, rect.height / 2f);
            }
            else if (minDistancePoint.Equals(upPoint))
            {
                firstRect = new Rect(0f, 0f, rect.width / 2f, rect.height);
                secondRect = new Rect(rect.width / 2f, 0f, rect.width / 2f, rect.height);
            } 
            else if (minDistancePoint.Equals(bottomPoint))
            {
                firstRect = new Rect(rect.width / 2f, 0f, rect.width / 2f, rect.height);
                secondRect = new Rect(0f, 0f, rect.width / 2f, rect.height);
            } 

            Vector2 firstRectPartDirection = Quaternion.Euler(0, 0, -rectPieceDirectionAngle) * (rect.center - minDistancePoint);
            Vector2 secondRectPartDirection = Quaternion.Euler(0, 0, rectPieceDirectionAngle) * (rect.center - minDistancePoint);

            SlicedRect slicedRect = new SlicedRect()
            {
                FirstRectPart = firstRect,
                SecondRectPart = secondRect,
                FirstRectPartDirection = firstRectPartDirection.normalized,
                SecondRectPartDirection = secondRectPartDirection.normalized
            };
            
            return slicedRect;
        }
    }

    public class SlicedRect
    {
        public Rect FirstRectPart;
        public Rect SecondRectPart;
        public Vector2 FirstRectPartDirection;
        public Vector2 SecondRectPartDirection;
    }
}