import { CategoryService } from './../../services/category.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Category } from '../../Models/Category';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [
    RouterModule,
    FormsModule,
    CommonModule
  ],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css'
})
export class MenuComponent implements OnInit {

  title = 'Menu';
  Categories: Category[] = [];

  constructor(private CategoryService: CategoryService) { }



  ngOnInit(): void {
      console.log('MenuComponent initialized');
      this.CategoryService.getAll().subscribe(x => {
        this.Categories = x;
      });

   }



}
