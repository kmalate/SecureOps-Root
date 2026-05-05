import { HttpInterceptorFn } from "@angular/common/http";

export const authInterceptor: HttpInterceptorFn = (req, next) => {
    const token = localStorage.getItem('accessToken');

    // If we have a token, clone the request and add the Authorization header
    if (token) {
        const cloned = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });

        return next(cloned);
    }

    // If no token, just let the request pass through as-is
    return next(req);
}