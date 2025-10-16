import { Component, inject, OnInit, signal } from '@angular/core';
import { FollowsService } from '../../core/services/follows-service';
import { Member } from '../../types/member';
import { MemberCard } from "../members/member-card/member-card";

@Component({
  selector: 'app-lists',
  imports: [MemberCard],
  templateUrl: './lists.html',
  styleUrl: './lists.css'
})
export class Lists implements OnInit{
  private followsService = inject(FollowsService);
  protected members = signal<Member []>([]);
  protected predicate = 'followed'

  tabs = [
    {label: 'You follow', value: 'followed'},
    {label: 'Follows you', value: 'followedBy'},
    {label: 'Mutual follow', value: 'mutual'}
  ]

  ngOnInit(): void {
    this.loadFollows();
  }

  setPredicate(predicate: string){
    if(this.predicate !== predicate){
      this.predicate = predicate;
      this.loadFollows();
    }
  }

  loadFollows(){
    this.followsService.getFollows(this.predicate).subscribe({
      next: members => this.members.set(members)
    })
  }
}
