@echo off
echo Excluindo todas as pastas 'bin' e 'obj' a partir do diret�rio atual...
echo.

:: Procura e deleta todas as pastas 'bin' e 'obj' dentro da estrutura de diret�rios
for /d /r . %%d in (bin,obj) do (
    if exist "%%d" (
        echo Deletando: %%d
        rd /s /q "%%d"
    )
)

echo.
echo Limpeza conclu�da!
pause
