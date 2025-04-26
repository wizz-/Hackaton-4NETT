Write-Host "Limpando publicações antigas..."

$apiPublish = "src/Hackaton.Api/publish"
$webPublish = "src/Hackaton.Web/publish"

if (Test-Path $apiPublish) {
    Remove-Item -Recurse -Force $apiPublish
    Write-Host "Removido: $apiPublish"
}

if (Test-Path $webPublish) {
    Remove-Item -Recurse -Force $webPublish
    Write-Host "Removido: $webPublish"
}

Write-Host "Construindo imagem da API..."
docker build -f "Dockerfile.Api" -t healthmed-api .

Write-Host "Construindo imagem do Blazor Web..."
docker build -f "Dockerfile.Web" -t healthmed-web .

Write-Host "Aplicando arquivos do Kubernetes..."
kubectl apply -f deploy.yaml

Write-Host "Aguardando pod do Blazor ficar pronto..."
do {
    Start-Sleep -Seconds 2
    $status = kubectl get pods -n healthmed -l app=web -o json | ConvertFrom-Json
    $phase = $status.items[0].status.phase
    Write-Host "Status: $phase"
} while ($phase -ne "Running")

Write-Host "Pod está pronto. Deploy concluído!"