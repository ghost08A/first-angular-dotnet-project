//ดักจับ Requaest ที่ส่งออกไปจากแอปพลิเคชัน เพื่อเพิ่ม Token ใน Header ของ Request

import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const token = localStorage.getItem('token'); // ดึง Token จาก Local Storage
  let requestToForward = req;
  if (token) {
    const clonedReq = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`, // เพิ่ม Token ใน Header ของ Request
      },
    });
    // ส่ง Request ที่แปะ Token แล้วไปทำงานต่อ
    return next(clonedReq);
  }
  //ถ้าไม่มี Token ก็ปล่อย Request ผ่านไปแบบปกติ (เช่น ตอนส่งหน้า Login)
  return next(requestToForward).pipe(
    catchError((error: HttpErrorResponse) => {
      // ถ้า .NET ตอบกลับมาว่า 401 Unauthorized (แปลว่า Token หมดอายุ, ไม่มี Token, หรือ Token ปลอม)
      if (error.status === 401) {
        // ล้างของเก่าที่อาจจะพังทิ้งไป
        window.localStorage.removeItem('token');

        // เตะผู้ใช้กลับไปหน้า Login ทันที
        router.navigate(['/login']);
      }

      // โยน Error ไปให้ Component จัดการต่อ (ถ้าจำเป็น)
      return throwError(() => error);
    }),
  );
};
