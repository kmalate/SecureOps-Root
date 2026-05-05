import { Injectable, signal, TemplateRef } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class Toast {
  private _toasts = signal<any[]>([]);
  toasts = this._toasts.asReadonly();

  show(textOrTpl: string | TemplateRef<any>, options: any = {}) {
    this._toasts.update((toasts) => [...toasts, { textOrTpl, ...options }]);
  }

  remove(toast: any) {
    this._toasts.update((toasts) => toasts.filter((t) => t !== toast));
  }
}
