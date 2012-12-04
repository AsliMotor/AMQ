@echo off
set date=%date:/=_%
set jam=%time::=_%
set BACKUP_FILE="D:\PostgreSQL_Backups\AsliMotor\AsliMotor_%date%_%jam%.backup"
SET PGPASSWORD = a1b2c3.
echo on
pg_dump -i -h localhost -p 5432 -U postgres -F c -b -v -f %BACKUP_FILE% aslimotor