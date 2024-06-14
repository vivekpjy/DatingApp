import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl;
  constructor(private http:HttpClient) { }

  getMembers():Observable<Member[]>{
    return this.http.get<Member[]>(this.baseUrl + 'users')
  }
  getMember(username:string){
    return this.http.get<Member>(this.baseUrl + 'users/' + username)
  }

  // used JWT interceptor instead of getHttpOptions() method 
  // getHttpOptions(){
  //   const userString = localStorage.getItem('user');
  //   if(!userString) return;
  //   const user = JSON.parse(userString);
  //   return {
  //     headers: new HttpHeaders({
  //       Authorization: 'Bearer ' + user.token
  //     })
  //   }

  // }
}
