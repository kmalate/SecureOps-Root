
import { Injectable, signal, computed } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  // Track simultaneous active HTTP requests
  private activeRequests = signal<number>(0);

  // Expose a read-only boolean state
  public isLoading = computed(() => this.activeRequests() > 0);

  show(): void {
    this.activeRequests.update(count => count + 1);
  }

  hide(): void {
    this.activeRequests.update(count => Math.max(0, count - 1));
  }
}
