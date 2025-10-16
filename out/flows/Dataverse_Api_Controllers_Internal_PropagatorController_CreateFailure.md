[web] POST /api/internal/Propagator/create-failure  (Dataverse.Api.Controllers.Internal.PropagatorController.CreateFailure)  [L65–L83] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository.Add [L82]
  └─ calls DataverseEntityFailureLogRepository.Remove [L79]
  └─ calls DataverseEntityFailureLogRepository.WriteQuery [L75]
  └─ delete DataverseEntityFailureLog [L79]
    └─ reads_from DataverseEntityFailureLogs
  └─ insert DataverseEntityFailureLog [L82]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L75]
    └─ reads_from DataverseEntityFailureLogs
  └─ uses_service IControlledRepository<DataverseEntityFailureLog>
    └─ method WriteQuery [L75]
      └─ ... (no implementation details available)

