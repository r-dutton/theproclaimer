[web] PUT /api/accounting/datasets/lock-state  (Cirrus.Api.Controllers.Accounting.DatasetsController.LockOrUnlock)  [L220–L226] status=200 [auth=user]
  └─ sends_request BulkUpdateDatasetsLockStateCommand [L223]
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.UpdateLockStateOfDatasetsCommandHandler.Handle [L26–L69]
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L46]
          └─ ... (no implementation details available)
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L53]
          └─ implementation IRequestProcessor.ProcessAsync [L9-L9]
          └─ ... (no implementation details available)

