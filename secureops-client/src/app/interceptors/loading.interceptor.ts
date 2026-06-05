import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { LoadingService } from '../services/loading';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingService = inject(LoadingService);

  // Turn on the loader when a request starts
  loadingService.show();

  return next(req).pipe(
    finalize(() => {
      // Turn off the loader when the request completes or throws an error
      loadingService.hide();
    })
  );
};