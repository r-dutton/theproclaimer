[web] GET /api/super/offices/{officeId:Guid}/audit  (Dataverse.Api.Controllers.Super.Core.OfficesController.GetUserAudit)  [L102–L127] [auth=Authentication.MasterPolicy,Authentication.RequireTenantIdPolicy]
  └─ calls OfficeRepository.ReadQuery [L105]
  └─ queries Office [L105]
    └─ reads_from Offices
  └─ uses_service IControlledRepository<Office>
    └─ method ReadQuery [L105]

