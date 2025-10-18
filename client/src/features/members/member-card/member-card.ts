import { Component, computed, inject, Inject, input } from '@angular/core';
import { Member } from '../../../types/member';
import { RouterLink } from '@angular/router';
import { FollowsService } from '../../../core/services/follows-service';
import { PresenceService } from '../../../core/services/presence-service';

@Component({
  selector: 'app-member-card',
  imports: [RouterLink],
  templateUrl: './member-card.html',
  styleUrl: './member-card.css'
})
export class MemberCard {
  private followService = inject(FollowsService);
  member = input.required<Member>();
  private presenceService = inject(PresenceService)
  protected isOnline = computed(() => this.presenceService.onlineUsers().includes(this.member().id))
  protected hasFollowed = computed(() => this.followService.followIds().includes(this.member().id));

  toggleFollow(event: Event){
    event.stopPropagation();
    this.followService.toggleFollow(this.member().id)
  }
}
