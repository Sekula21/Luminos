import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { filter, single } from 'rxjs';
import { Member } from '../../../types/member';
import { AgePipe } from '../../../core/pipes/age-pipe';
import { AccountService } from '../../../core/services/account-service';
import { MemberService } from '../../../core/services/member-service';
import { PresenceService } from '../../../core/services/presence-service';
import { FollowsService } from '../../../core/services/follows-service';

@Component({
  selector: 'app-member-detailed',
  imports: [RouterLink, RouterLinkActive, RouterOutlet, AgePipe],
  templateUrl: './member-detailed.html',
  styleUrl: './member-detailed.css'
})
export class MemberDetailed implements OnInit{
  private route = inject(ActivatedRoute);
  protected memberService = inject(MemberService)
  protected presenceService = inject(PresenceService)
  private accountService = inject(AccountService)
  protected followService = inject(FollowsService)
  private router = inject(Router);
  protected title = signal<string | undefined>('Profile');
  private routeId = signal<string | null>(null);
  protected isCurrentUser = computed(() => {
    return this.accountService.currentUser()?.id === this.routeId()
  });
  protected hasFollowed = computed(() => this.followService.followIds().includes(this.routeId()!));

  constructor(){
    this.route.paramMap.subscribe(params => {
      this.routeId.set(params.get('id'));
    })
  }

  ngOnInit(): void {
    this.title.set(this.route.firstChild?.snapshot?.title);

    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe({
      next: () => {
        this.title.set(this.route.firstChild?.snapshot?.title);
      }
    })
  }
}
