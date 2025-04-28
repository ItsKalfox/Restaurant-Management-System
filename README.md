# Restaurant Management System

A collaborative project for **CS107.3 - Object Oriented Programming with C#**, developed by Group BI.

---

## Project Description

This system provides end-to-end restaurant management capabilities, including:

- Table booking and real-time status monitoring
- Order and billing management
- Staff and shift scheduling
- Customer feedback and rating system
- Salary & tip management
- Analytics dashboard (performance, sales, etc.)
- Newsletter and promotion email system

---

## Tech Stack

| Layer | Tech |
|-------|------|
| Frontend | WinForms (.NET) |
| Backend | C# |
| Database | MySQL |
| Version Control | Git & GitHub |

---

## How to Run the Project

1. Clone the repo:
   ```bash
   git clone https://github.com/ItsKalfox/Restaurant-Management-System.git
   ```
2. Open the solution in Visual Studio
3. Restore NuGet packages if needed
4. Connect to the MySQL Database
5. Build and run!

---

## Test Task Instructions for Team Members

This is a practice task to help everyone get comfortable with Git, branches, and pull requests before we start working on the actual project.

### Objective:
Add your full name and university ID to the `members.txt` file inside the `GitTest` folder by creating a new branch and submitting a pull request (PR) to the `dev` branch.

### Step-by-step Instructions:

*(Optional: If you want to clone the repo in a custom location, first navigate to your custom location using cd commands and follow below instructions.)*
#### 1. Clone the repository  
```bash
git clone https://github.com/ItsKalfox/Restaurant-Management-System.git
```  
Clones the GitHub repository to your local machine.

#### 2. Navigate into the project folder  
```bash
cd Restaurant-Management-System
```  
Changes directory to your cloned project.

#### 3. Fetch and switch to the latest `dev` branch  
```bash
git fetch origin
```  
Fetches the latest branches from GitHub.  
```bash
git checkout -b dev origin/dev
```  
Creates and switches to a local `dev` branch that tracks the remote `dev`.

#### 4. Create your own branch from `dev`  
```bash
git checkout -b yourBranchName
```  
*(Example: `git checkout -b kalfox`)*  
Creates a new branch from `dev` and switches to it. Use your short name for the branch name.

#### 5. Edit `members.txt`  
Open the file `GitTest/members.txt` and add a new line at the bottom with:  
`Name With Initials - University ID`

#### 6. Add the file to staging  
```bash
git add GitTest/members.txt
```
Stages the updated file so it's ready for committing.

#### 7. Commit your changes  
```bash
git commit -m "Added my name and university ID"
```  
Saves the change locally with a commit message.

#### 8. Push your branch to GitHub  
```bash
git push origin yourBranchName
```  
Pushes your feature branch to GitHub.

#### 9. Create a Pull Request  
- Go to the GitHub repository in your browser.  
- Click on “Compare & pull request”.  
- Base branch should be `dev`, and compare should be your branch.  
- Click “Create pull request”.

---

## ❗Important Guidelines for All Contributors  
- Always create your feature branches from the `dev` branch, NOT from `main`.
- Do NOT make changes directly on the `main` branch.
- When you're ready to contribute, create a Pull Request (PR) to the `dev` branch.
- Your PR will be reviewed and tested before being merged into `dev`.
- Once all features are tested and verified, I will merge them into `main` branch.

Once your PR is merged into `dev`, you’ve successfully completed the test task. Well done! 🎉  
If you get stuck, message me directly or drop a note in the group chat! 👍🦊