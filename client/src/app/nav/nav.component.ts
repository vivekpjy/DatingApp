import { Component, OnInit } from '@angular/core';
import {AccountService} from '../_services/account.service'
import { Observable,of } from 'rxjs';
import { User } from '../_models/user';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model:any={};
  constructor(public service:AccountService, private router:Router,
    private toastr:ToastrService
  ){}

  ngOnInit(): void {
    console.log(this.service.currentUser$)
  }

 
  login(){
    this.service.login(this.model).subscribe(
      {
        next:()=> this.router.navigateByUrl('/members')
      }
    )
  }

  logout(){
    this.service.logout();
    this.router.navigateByUrl('/')

  }

}
