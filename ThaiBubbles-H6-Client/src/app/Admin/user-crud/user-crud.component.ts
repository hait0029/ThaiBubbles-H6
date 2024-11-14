import { UserService } from './../../services/user.service';
import { FormsModule } from '@angular/forms';
import { User } from './../../Models/User';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-crud',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './user-crud.component.html',
  styleUrl: './user-crud.component.css'
})
export class UserCrudComponent {

  userArray: User[] = [];
  user: User = { userID: 0, email: '', password: '', fName: '', lName: '', phoneNr: 0, address: '', cityId: 0, roleID:0
   };
  userCopy: User = { ...this.user }; // Temporary object for form data

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.getAll().subscribe((users) => {
      this.userArray = users;
    });
  }

  edit(selectedUser: User): void {
    this.user = selectedUser;
    this.userCopy = { ...selectedUser }; // Create a copy to prevent direct binding
  }

  delete(selectedUser: User): void {
    if (confirm('Are you sure you want to delete this user?')) {
      this.userService.delete(selectedUser.userID).subscribe({
        next: () => {
          this.userArray = this.userArray.filter((user) => user.userID !== selectedUser.userID);
        }
      });
    }
  }

  save(): void {
    if (this.userCopy.userID === 0) {
      this.userService.createcrud(this.userCopy).subscribe({
        next: (newUser) => {
          this.userArray.push(newUser);
          this.resetForm();
        },
        error: (err) => {
          console.log(Object.keys(err.error.errors).join(', '));
        }
      });
    } else {
      this.userService.update(this.userCopy).subscribe({
        next: () => {
          const index = this.userArray.findIndex(u => u.userID === this.userCopy.userID);
          if (index !== -1) {
            this.userArray[index] = { ...this.userCopy }; // Update list with saved data
          }
          this.resetForm();
        },
        error: (err) => {
          console.log(Object.keys(err.error.errors).join(', '));
        }
      });
    }
  }

  resetForm(): void {
    this.user = { userID: 0, email: '', password: '', fName: '', lName: '', phoneNr: 0, address: '', cityId: 0, roleID:0 };
    this.userCopy = { ...this.user };
  }
}
