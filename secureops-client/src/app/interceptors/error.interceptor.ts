import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";
import { catchError, delay, throwError } from "rxjs";
import { Toast } from "../services/toast";

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
    const toast = inject(Toast);

    return next(req).pipe(
        catchError((error) => {
            // Handle 401 Unauthorized (e.g., redirect to login)
            if (error.status === 401) {
                console.error("Session expired");
            }

            // Handle Fluent Validation Errors (400 Bad Request)
            if (error.status === 400 && error.error) {
                const messages = error.error.map((err: { errorMessage: any; }) => err.errorMessage);
                toast.show(messages.join(' '), { classname: 'bg-danger text-light', delay: 5000 });
            } else {
                // Generic server error
                toast.show(error.error?.message || "A server error occurred.", { classname: 'bg-danger text-light', delay: 5000 });
            }

            return throwError(() => error);
        })
    );
};