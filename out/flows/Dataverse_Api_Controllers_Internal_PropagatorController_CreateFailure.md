[web] POST /api/internal/Propagator/create-failure  (Dataverse.Api.Controllers.Internal.PropagatorController.CreateFailure)  [L65–L83] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.Add [L82]
  └─ calls DataverseEntityFailureLogRepository.Remove [L79]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L75]
  └─ writes_to DataverseEntityFailureLog [L82]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L79]
    └─ reads_from DataverseEntityFailureLogs
  └─ writes_to DataverseEntityFailureLog [L75]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L75]

