import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink } from "@angular/router";
import { RouterLinkActive } from "@angular/router";
import { ToastService } from '../../core/services/toast-service';
import { BusyService } from '../../core/services/busy-service';
import { HasRole } from '../../shared/directives/has-role';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive, HasRole],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  protected accountService = inject(AccountService)
  protected busyService = inject(BusyService);
  private router = inject(Router)
  private toast = inject(ToastService)
  protected creds: any = {}
  protected loading = signal(false);

  login(){
    this.loading.set(true);
    this.accountService.login(this.creds).subscribe({
      next: () => {
        this.router.navigateByUrl('/users');
        this.toast.success('You re logged in!')
        this.creds = {}
      },
      error: error => {
        this.toast.error(error.error)
      },
      complete: () => this.loading.set(false)
    })
  }
  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }

  handleSelectUserItem(){
    const elem = document.activeElement as HTMLDivElement;
    if(elem) elem.blur();
  }
}
