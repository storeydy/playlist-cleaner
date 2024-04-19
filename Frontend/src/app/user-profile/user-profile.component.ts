import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { TabViewModule } from 'primeng/tabview';
import { UserProfileService } from '../shared/data-access/user-profile/user-profile.service';
import { Subscription } from 'rxjs';
import { GetCurrentUsersProfileResponse } from '../shared/types/openapi';
import { Router } from '@angular/router';

@Component({
  selector: 'playlist-cleaner-user-profile',
  standalone: true,
  imports: [CommonModule, CardModule, ButtonModule, TabViewModule ],
  templateUrl: './user-profile.component.html',
  styleUrl: './user-profile.component.scss',
})
export class UserProfileComponent {

  private readonly userProfileService = inject(UserProfileService);
  private subscription = new Subscription();

  activeIndex: number = 0;
  profileData: GetCurrentUsersProfileResponse | null = null;

  constructor(private router: Router) {}

  async ngOnInit() {    
    this.initialiseSubscriptions();
    this.userProfileService.getUserProfile();
  }


  private initialiseSubscriptions() {
    this.subscription.add(
      this.userProfileService.profileObject$.subscribe((res) => {
        this.profileData = res;
      })
    );

  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  navigateToWelcome(){
    this.router.navigate(['']);
  }
}
