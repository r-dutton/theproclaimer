[web] POST /api/internal/Propagator/create-failure  (Dataverse.Api.Controllers.Internal.PropagatorController.CreateFailure)  [L65–L83] status=201 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls DataverseEntityFailureLogRepository (methods: Add,Remove,WriteQuery) [L82]
  └─ insert DataverseEntityFailureLog [L82]
    └─ reads_from DataverseEntityFailureLogs
  └─ delete DataverseEntityFailureLog [L79]
    └─ reads_from DataverseEntityFailureLogs
  └─ write DataverseEntityFailureLog [L75]
    └─ reads_from DataverseEntityFailureLogs
  └─ impact_summary
    └─ entities 1 (writes=3, reads=0)
      └─ DataverseEntityFailureLog writes=3 reads=0

