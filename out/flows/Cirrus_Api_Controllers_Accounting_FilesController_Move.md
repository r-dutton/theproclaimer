[web] PUT /api/accounting/files/{id:Guid}/move  (Cirrus.Api.Controllers.Accounting.FilesController.Move)  [L260–L265] [auth=user]
  └─ calls FileRepository.WriteQuery [L263]
  └─ writes_to File [L263]
    └─ reads_from Files
  └─ uses_service IControlledRepository<File>
    └─ method WriteQuery [L263]
  └─ sends_request MoveFileCommand [L264]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Cirrus.ApplicationService.Accounting.Commands.MoveFileCommandHandler.Handle [L36–L118]
      └─ uses_service IControlledRepository<File>
        └─ method ReadQuery [L53]
      └─ uses_service IRequestProcessor (InstancePerDependency)
        └─ method ProcessAsync [L54]
      └─ uses_service IUserService (InstancePerLifetimeScope)
        └─ method GetUserId [L57]

