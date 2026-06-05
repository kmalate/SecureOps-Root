import { Component } from '@angular/core';
import { Footer } from '../footer/footer';
import { Sidebar } from '../sidebar/sidebar';
import { Header } from '../header/header';
import { RouterOutlet } from '@angular/router';
import { ToastContainer } from '../../toast-container/toast-container';

@Component({
  selector: 'app-main-layout',
  imports: [Footer, Header, Sidebar, RouterOutlet, ToastContainer],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css',
})
export class MainLayout {
  isSidebarVisible = false;

  toggleSidebar() {
    this.isSidebarVisible = !this.isSidebarVisible;
  }
}
