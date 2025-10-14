[web] GET /api/internal/cabinets/default  (Dataverse.Api.Controllers.Internal.Documents.CabinetsController.GetDefault)  [L32–L42] [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ maps_to CabinetDto [L35]
    └─ automapper.registration DataverseMappingProfile (Cabinet->CabinetDto) [L397]
  └─ calls CabinetRepository.ReadQuery [L35]
  └─ queries Cabinet [L35]
    └─ reads_from Cabinets
  └─ uses_service IControlledRepository<Cabinet>
    └─ method ReadQuery [L35]

