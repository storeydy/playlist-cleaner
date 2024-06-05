import { Component, Input, OnInit, inject } from '@angular/core';
import { UserProfileService } from '../../data-access/user-profile/user-profile.service';
import { GetCurrentUsersProfileResponse } from '../../types/openapi';
import { ButtonModule } from 'primeng/button';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'playlist-cleaner-header',
  standalone: true,
  imports: [CommonModule, ButtonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
})
export class HeaderComponent implements OnInit {

  @Input() headerText: string = "";
  
  private readonly userProfileService = inject(UserProfileService);

  private subscription = new Subscription();

  profileData: GetCurrentUsersProfileResponse | null = null;

  constructor(private router: Router) { }

  async ngOnInit() {
    this.initialiseSubscriptions();
    this.userProfileService.getUserProfile();
  }

  private initialiseSubscriptions(){
    this.subscription.add(
      this.userProfileService.profileObject$.subscribe((res) => {
        this.profileData = res;
      })
    )
  }

  navigateToWelcome() {
    this.router.navigate(['']);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
