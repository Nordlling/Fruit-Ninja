using System;
using UnityEngine;

namespace Main.Scripts.Utils.RectUtils
{
    public static class RectUtils
    {
        
        public static SlicedRect GetSlicedRect(this Rect rect, Vector2 pointPosition, Vector2 pointDirection)
        {
            float offsetStep = 0.1f;
            SlicedRect slicedRect = new SlicedRect();
            Vector2 directionOffset = pointDirection.normalized * offsetStep;

            if (!rect.Contains(pointPosition) || directionOffset == Vector2.zero)
            {
                // Debug.LogWarning("Incorrect input data");
                CreateLeftSideSlicedRect(rect, slicedRect);
                return slicedRect;
            }

            int stepCount = (int)((rect.width + rect.height) / offsetStep);
            for (int i = 0; i < stepCount; i++)
            {
                pointPosition += directionOffset;
                
                if (Math.Abs(pointPosition.x - rect.xMin) <= offsetStep)
                {
                    CreateLeftSideSlicedRect(rect, slicedRect);
                    break;
                } 
                
                if (Math.Abs(pointPosition.x - rect.xMax) <= offsetStep)
                {
                    CreateRightSideSlicedRect(rect, slicedRect);
                    break;
                }
                
                if (Math.Abs(pointPosition.y - rect.yMin) <= offsetStep)
                {
                    CreateBottomSideSlicedRect(rect, slicedRect);
                    break;
                }
                
                if (Math.Abs(pointPosition.y - rect.yMax) <= offsetStep)
                {
                    CreateUpperSideSlicedRect(rect, slicedRect);
                    break;
                }
            }
            
            return slicedRect;
        }

        private static void CreateLeftSideSlicedRect(Rect rect, SlicedRect slicedRect)
        {
            slicedRect.FirstRectPart = new Rect(0f, rect.height / 2f, rect.width, rect.height / 2f);
            slicedRect.SecondRectPart = new Rect(0f, 0f, rect.width, rect.height / 2f);
            slicedRect.FirstPivot = new Vector2(0.5f, 0f);
            slicedRect.SecondPivot = new Vector2(0.5f, 1f);
        }
        
        private static void CreateRightSideSlicedRect(Rect rect, SlicedRect slicedRect)
        {
            slicedRect.FirstRectPart = new Rect(0f, 0f, rect.width, rect.height / 2f);
            slicedRect.SecondRectPart = new Rect(0f, rect.height / 2f, rect.width, rect.height / 2f);
            slicedRect.FirstPivot =  new Vector2(0.5f, 1f);
            slicedRect.SecondPivot = new Vector2(0.5f, 0f);
        }
        
        private static void CreateBottomSideSlicedRect(Rect rect, SlicedRect slicedRect)
        {
            slicedRect.FirstRectPart = new Rect(0f, 0f, rect.width / 2f, rect.height);
            slicedRect.SecondRectPart = new Rect(rect.width / 2f, 0f, rect.width / 2f, rect.height);
            slicedRect.FirstPivot = new Vector2(1f, 0.5f);
            slicedRect.SecondPivot = new Vector2(0f, 0.5f);
        }
        
        private static void CreateUpperSideSlicedRect(Rect rect, SlicedRect slicedRect)
        {
            slicedRect.FirstRectPart = new Rect(rect.width / 2f, 0f, rect.width / 2f, rect.height);
            slicedRect.SecondRectPart = new Rect(0f, 0f, rect.width / 2f, rect.height);
            slicedRect.FirstPivot =  new Vector2(0f, 0.5f);
            slicedRect.SecondPivot = new Vector2(1f, 0.5f);
        }
    }

    public class SlicedRect
    {
        public Rect FirstRectPart;
        public Rect SecondRectPart;
        public Vector2 FirstRectPartDirection;
        public Vector2 SecondRectPartDirection;
        public Vector2 FirstPivot;
        public Vector2 SecondPivot;
    }
}