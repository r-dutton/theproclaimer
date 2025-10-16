[web] POST /api/internal/data-lake/process-financial-data  (Dataverse.Api.Controllers.Internal.DataLakeController.ProcessFinancialData)  [L37–L51] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ uses_service IDocumentStoreService (AddScoped)
    └─ method GetReadOnlyDocumentStoresWithCredentials [L43]
      └─ implementation Dataverse.ApplicationService.Services.DocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L17-L89]
  └─ uses_storage IDocumentStoreService.GetReadOnlyDocumentStoresWithCredentials [L43]

