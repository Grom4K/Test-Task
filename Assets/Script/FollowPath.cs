//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;



//namespace Assets.Script
//{
//    class FollowPath : MonoBehaviour
//    {
//        // speed is the rate at which the object will rotate
//        public float speed;

//        void FixedUpdate()
//        {
//            // Сгенерируйте плоскость, пересекающую положение преобразования с восходящей нормалью.

//            Plane playerPlane = new Plane(Vector3.up, transform.position);

//            // Сгенерируйте луч из положения курсора

//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//            / Определите точку, в которой луч курсора пересекает плоскость.
//// Это будет точка, в которую объект должен смотреть, чтобы смотреть на мышь.
//// Raycasting к плоскому объекту дает нам только расстояние, поэтому нам придется взять это расстояние,
/// затем найдите точку вдоль этого луча, которая встречается с этим расстоянием.В этом и будет смысл
//// чтобы посмотреть.
//            float hitdist = 0.0f;
//            // If the ray is parallel to the plane, Raycast will return false.
//            if (playerPlane.Raycast(ray, out hitdist))
//            {
//                // Get the point along the ray that hits the calculated distance.
//                Vector3 targetPoint = ray.GetPoint(hitdist);

//                // Определите вращение цели. Это вращение, если преобразование смотрит на целевую точку.
//                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

//                // Плавно вращайтесь в направлении целевой точки.
//                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
//            }
//        }
//    }
//}
