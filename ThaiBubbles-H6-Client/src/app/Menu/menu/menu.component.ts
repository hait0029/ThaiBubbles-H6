import { CategoryService } from './../../services/category.service';
import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { Category } from '../../Models/Category';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css',
})
export class MenuComponent implements OnInit {
  title = 'Menu';
  Categories: Category[] = [];

  images = ['assets/bubbleteaicon.png', 'assets/unnamed.png'];

  constructor(private CategoryService: CategoryService) {}

  getRandomImage(): string {
    // Vælg et tilfældigt billede fra listen
    const randomIndex = Math.floor(Math.random() * this.images.length);
    return this.images[randomIndex];
  }

  ngOnInit(): void {
    console.log('MenuComponent initialized');
    this.CategoryService.getAll().subscribe((x) => {
      this.Categories = x;
    });
  }
}
