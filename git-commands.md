# Git Command Journey

This document outlines the git commands used during the session to manage the C# project repository.

## Initial Setup

### Configure Git User Information
```bash
git config --global user.name "Paul Anthony Begley"
git config --global user.email paulanthonybegley@gmail.com
```
Set the global git user name and email for commit attribution.

### Increase HTTP Post Buffer
```bash
git config http.postBuffer 524288000
```
Increased the HTTP post buffer size to handle large pushes.

## Repository Status and Basic Operations

### Check Repository Status
```bash
git status
```
Used frequently to check the state of the working directory and staging area.

### Add Files to Staging
```bash
git add .
```
Add all new and modified files to the staging area.

```bash
git add journey.md journey.pdf
```
Add specific files to the staging area.

### Commit Changes
```bash
git commit -m "Add .gitignore and initial project files"
```
Create a commit with a descriptive message.

```bash
git commit -m "Add journey documentation files"
```
Commit the journey documentation files.

### Push to Remote Repository
```bash
git push origin main
```
Push local commits to the remote repository's main branch.

```bash
git push origin main --force-with-lease
```
Force push with lease to safely overwrite remote history after amendments.

## Viewing History

### View Commit History
```bash
git log --oneline
```
View a condensed history of commits.

```bash
git log --all --full-history -- journey.md
```
Check the history of a specific file across all branches.

## Advanced Git Operations

### Amending Commits
```bash
git commit --amend -m "Add journey documentation files" --reset-author
```
Amend the most recent commit to update the author information and commit message.

```bash
git commit --amend -m "Add .gitignore and initial project files" --reset-author
```
Amend a commit to fix the author information.

### Reset Operations
```bash
git reset --soft d65fcb7
```
Reset HEAD to a specific commit while keeping changes staged.

```bash
git reset --hard 88b7a6d
```
Reset HEAD to a specific commit, discarding all changes.

### Branching and Checkout
```bash
git checkout d65fcb7
```
Checkout a specific commit (results in detached HEAD state).

```bash
git checkout main
```
Switch back to the main branch.

### Rebasing
```bash
git rebase --onto 982f2a6 d65fcb7 main
```
Rebase the main branch onto a corrected commit.

### Remote Repository Management
```bash
git remote -v
```
View the remote repositories configured for the local repository.

## Summary of Key Commands Used

1. **Configuration**: `git config` for user info and buffer size
2. **Status**: `git status` to check repository state
3. **Staging**: `git add` to prepare files for commit
4. **Committing**: `git commit` to save changes
5. **Amending**: `git commit --amend` to fix previous commits
6. **Pushing**: `git push` to share changes with remote
7. **History**: `git log` to review commit history
8. **Reset**: `git reset` to move HEAD pointer
9. **Checkout**: `git checkout` to switch branches/commits
10. **Rebase**: `git rebase` to rewrite commit history
11. **Remote**: `git remote` to manage remote repositories

This journey demonstrates a complete workflow from initial setup through committing, amending history, and pushing to a remote repository.