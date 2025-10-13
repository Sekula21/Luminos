import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../../core/services/account-service';
import { Router, RouterLink } from "@angular/router";
import { RouterLinkActive } from "@angular/router";
import { ToastService } from '../../core/services/toast-service';
import { BusyService } from '../../core/services/busy-service';

@Component({
  selector: 'app-nav',
  imports: [FormsModule, RouterLink, RouterLinkActive],
  templateUrl: './nav.html',
  styleUrl: './nav.css'
})
export class Nav {
  protected accountService = inject(AccountService)
  protected busyService = inject(BusyService);
  private router = inject(Router)
  private toast = inject(ToastService)
  protected creds: any = {}

  login(){
    this.accountService.login(this.creds).subscribe({
      next: () => {
        this.router.navigateByUrl('/users');
        this.toast.success('You re logged in!')
        this.creds = {}
      },
      error: error => {
        this.toast.error(error.error)
      }
    })
  }
  logout(){
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
