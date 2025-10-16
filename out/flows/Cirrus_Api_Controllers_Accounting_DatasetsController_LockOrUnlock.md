[web] PUT /api/accounting/datasets/lock-state  (Cirrus.Api.Controllers.Accounting.DatasetsController.LockOrUnlock)  [L220–L226] status=200 [auth=user]
  └─ sends_request BulkUpdateDatasetsLockStateCommand -> UpdateLockStateOfDatasetsCommandHandler [L223]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.UpdateLockStateOfDatasetsCommandHandler.Handle [L26–L69]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L53]
          └─ implementation DataGet.Services.Features.Requests.RequestProcessor.ProcessAsync [L7-L35]
            └─ ... (no dispatches detected)
      └─ uses_service IControlledRepository<Dataset> (Scoped (inferred))
        └─ method WriteQuery [L46]
          └─ implementation Cirrus.Data.Repository.Accounting.DatasetRepository.WriteQuery
  └─ impact_summary
    └─ requests 1
      └─ BulkUpdateDatasetsLockStateCommand
    └─ handlers 1
      └─ UpdateLockStateOfDatasetsCommandHandler

