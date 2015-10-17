@echo off 

IF NOT "%VS140COMNTOOLS%" == "" (call "%VS140COMNTOOLS%vsvars32.bat")


for /F %%A in ('dir /b src\*.sln') do call devenv src\%%A /Rebuild "Release" 
