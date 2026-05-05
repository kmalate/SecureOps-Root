import { Component, inject, TemplateRef } from '@angular/core';
import { Toast } from '../../services/toast';
import { NgbToastModule } from '@ng-bootstrap/ng-bootstrap';
import { NgTemplateOutlet } from '@angular/common';

@Component({
  selector: 'app-toast-container',
  imports: [NgbToastModule, NgTemplateOutlet],
  templateUrl: './toast-container.html',
  styleUrl: './toast-container.css',
})
export class ToastContainer {
  toastService = inject(Toast);

  isTemplate(toast: any) {
    return toast.textOrTpl instanceof TemplateRef;
  }
}
