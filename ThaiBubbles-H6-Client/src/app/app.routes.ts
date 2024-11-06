import { Routes } from '@angular/router';
import { HomeComponent } from './Frontpage/home/home.component';
import { MenuComponent } from './Menu/menu/menu.component';
import { LoginComponent } from './Login/login/login.component';
import { SignupComponent } from './SignUp/signup/signup.component';
import { ProductsComponent } from './Product/products/products.component';
import { AboutComponent } from './About/about/about.component';

export const routes: Routes = [
    {path:'', component:HomeComponent},
    {path:'menu', component:MenuComponent},
    {path:'about', component:AboutComponent},
    {path:'category/:categoryID',component:ProductsComponent},
    
    {path:'login',component:LoginComponent},
    {path:'signup',component:SignupComponent}
];
