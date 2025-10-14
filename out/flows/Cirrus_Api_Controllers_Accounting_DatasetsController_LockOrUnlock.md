[web] PUT /api/accounting/datasets/lock-state  (Cirrus.Api.Controllers.Accounting.DatasetsController.LockOrUnlock)  [L220–L226] [auth=user]
  └─ sends_request BulkUpdateDatasetsLockStateCommand [L223]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.UpdateLockStateOfDatasetsCommandHandler.Handle [L26–L69]
      └─ uses_service IControlledRepository<Dataset>
        └─ method WriteQuery [L46]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L53]

