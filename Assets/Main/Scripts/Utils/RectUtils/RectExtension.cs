using System;
using UnityEngine;

namespace Main.Scripts.Utils.RectUtils
{
    public static class RectUtils
    {
        // TODO: will be optimized
        public static SlicedRect GetSlicedRect(this Rect rect, Vector2 pointPosition, Vector2 pointDirection,
            float rectPieceDirectionAngle)
        {
            
            Rect firstRect;
            Rect secondRect;
            Vector2 firstRectPartDirection;
            Vector2 secondRectPartDirection;
            
            Vector2 offset = -pointDirection.normalized * 0.1f;
            
            while (true)
            {
                pointPosition += offset;
                if (Math.Abs(pointPosition.x - rect.xMin) < 0.1f)
                {
                    firstRect = new Rect(0f, 0f, rect.width, rect.height / 2f);
                    secondRect = new Rect(0f, rect.height / 2f, rect.width, rect.height / 2f);
                    firstRectPartDirection = Quaternion.Euler(0, 0, -rectPieceDirectionAngle) * pointDirection;
                    secondRectPartDirection = Quaternion.Euler(0, 0, rectPieceDirectionAngle) * pointDirection;
                    break;
                } 
                
                if (Math.Abs(pointPosition.x - rect.xMax) < 0.1f)
                {
                    firstRect = new Rect(0f, rect.height / 2f, rect.width, rect.height / 2f);
                    secondRect = new Rect(0f, 0f, rect.width, rect.height / 2f);
                    firstRectPartDirection = Quaternion.Euler(0, 0, -rectPieceDirectionAngle) * pointDirection;
                    secondRectPartDirection = Quaternion.Euler(0, 0, rectPieceDirectionAngle) * pointDirection;
                    break;
                }
                
                if (Math.Abs(pointPosition.y - rect.yMin) < 0.1f)
                {
                    firstRect = new Rect(0f, 0f, rect.width / 2f, rect.height);
                    secondRect = new Rect(rect.width / 2f, 0f, rect.width / 2f, rect.height);
                    firstRectPartDirection = Quaternion.Euler(0, 0, rectPieceDirectionAngle) * pointDirection;
                    secondRectPartDirection = Quaternion.Euler(0, 0, -rectPieceDirectionAngle) * pointDirection;
                    break;
                }
                
                if (Math.Abs(pointPosition.y - rect.yMax) < 0.1f)
                {
                    firstRect = new Rect(rect.width / 2f, 0f, rect.width / 2f, rect.height);
                    secondRect = new Rect(0f, 0f, rect.width / 2f, rect.height);
                    firstRectPartDirection = Quaternion.Euler(0, 0, rectPieceDirectionAngle) * pointDirection;
                    secondRectPartDirection = Quaternion.Euler(0, 0, -rectPieceDirectionAngle) * pointDirection;
                    break;
                }
            }

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