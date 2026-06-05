# SecureOps 🛡️

SecureOps is a modern, full-stack security incident reporting and management application. It empowers security teams to log, track, modify, and manage operational incidents in real-time, ensuring streamlined communication and robust digital logging for physical security environments.

## 🚀 Live Demo & Deployment
* **Frontend (UI):** [[Link to Azure Static Web App]](https://gentle-tree-0dbdeb30f.7.azurestaticapps.net/)
* **Backend (API):** [[Link to Azure App Service](https://secureops-api-eqcah5bbggcxgnc3.eastus-01.azurewebsites.net/WeatherForecast)]

Demo Credentials  
Email: demo@secureops.app  
Password: SecureOpsDemo2026!  

---

## 🛠️ Tech Stack

### Frontend
* **Framework:** Angular (v21+)
* **Styling & UI Components:** Bootstrap (for crisp, responsive)

### Backend
* **Framework:** .NET Core Web API (C#)
* **Authentication:** JWT (JSON Web Tokens) with ASP.NET Core Identity
* **ORM:** Entity Framework Core

### Database & Cloud Hosting
* **Database:** PostgreSQL (Hosted via Supabase)
* **API Hosting:** Azure App Service
* **Client Hosting:** Azure Static Web Apps

---

## 🔑 Key Features & Core Functionality

* **Secure Authentication:** Robust user login powered by .NET Authentication and JWT bearer tokens to secure endpoint access.
* **Incident Dashboard:** A clean, scannable list view displaying all reported incidents with status indicators.
* **Full CRUD Operations:** * **Create:** Dynamic forms to report new security incidents.
    * **Read:** Detailed view of specific incident parameters.
    * **Update:** Edit existing incident reports to update status, descriptions, or notes.
    * **Delete:** Remove entries from the active tracker (restricted access).
* **Responsive Layout:** Fully optimized for desktop workstations and mobile devices on mobile patrol.

---

## 📈 Planned Enhancements (Roadmap)

To elevate SecureOps from an incident tracker to an enterprise-grade operations platform, the following features are slated for development:

* [ ] **Role-Based Access Control (RBAC):** Distinct permissions for Officers (create/view) and Supervisors/Dispatch (edit/delete/assign).
* [ ] **Analytics & Reporting Dashboard:** Visual charts (using Chart.js or Ngx-charts) to map incident trends, peak times, and high-activity zones.
* [ ] **Media Attachments:** Ability to upload and attach scene photos or document copies directly to an incident report using cloud storage.

---

## 🏗️ Architecture Overview

SecureOps follows a decoupled, client-server architecture:
