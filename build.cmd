@echo off

if not exist .paket\paket.exe (
  .paket\paket.bootstrapper.exe
)

.paket\paket.exe restore
if errorlevel 1 (
  exit /b %errorlevel%
)

set encoding=utf-8
packages\FAKE\tools\FAKE.exe build.fsx %*