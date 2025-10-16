import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Member } from '../../types/member';

@Injectable({
  providedIn: 'root'
})
export class FollowsService {
  private baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  followIds = signal<string[]>([]);

  toggleFollow(targetMemberId: string){
    return this.http.post(`${this.baseUrl}follows/${targetMemberId}`, {});
  }

  getFollows(predicate: string){
    return this.http.get<Member[]>(this.baseUrl + 'follows?predicate=' + predicate);
  }

  getFollowIds(){
    return this.http.get<string[]>(this.baseUrl + 'follows/list').subscribe({
      next: ids => this.followIds.set(ids)
    })
  }

  clearFollowIds(){
    this.followIds.set([]);
  }
}
