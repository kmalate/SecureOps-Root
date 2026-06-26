import { Component, EventEmitter, Input, Output, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { Modal } from 'bootstrap';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  templateUrl: './confirm-dialog.html'
})
export class ConfirmDialogComponent implements AfterViewInit {
  @Input() title = 'Confirm';
  @Input() message = 'Are you sure?';

  @Output() confirmed = new EventEmitter<void>();

  @ViewChild('modal') modalElement!: ElementRef;
  private modal!: Modal;

  ngAfterViewInit() {
    this.modal = new Modal(this.modalElement.nativeElement);
  }

  open() {
    this.modal.show();
  }

  confirm() {
    this.confirmed.emit();
    this.modal.hide();
  }
}
