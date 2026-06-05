
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ToastContainer } from './components/toast-container/toast-container';
import { Spinner } from './components/spinner/spinner';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, ToastContainer, Spinner],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {protected readonly title = signal('secureops-client');
}
