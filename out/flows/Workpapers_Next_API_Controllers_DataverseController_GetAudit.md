[web] GET /api/dataverse/{entityRoute}/audit-entity/{dataverseId}  (Workpapers.Next.API.Controllers.DataverseController.GetAudit)  [L385–L395] status=200 [auth=AuthorizationPolicies.M2M,AuthorizationPolicies.RequireTenantId]
  └─ sends_request DataverseEntityAuditQuery -> DataverseEntityAuditQueryHandler [L392]
    └─ handled_by Cirrus.ApplicationService.Firm.Queries.DataverseEntityAuditQueryHandler.Handle [L24–L57]
      └─ uses_service IControlledRepository<Entity> (Scoped (inferred))
        └─ method ReadQuery [L52]
          └─ implementation Cirrus.Data.Repository.Firm.EntityRepository.ReadQuery
      └─ uses_service IControlledRepository<Client> (Scoped (inferred))
        └─ method ReadQuery [L50]
          └─ implementation Cirrus.Data.Repository.Firm.ClientRepository.ReadQuery
      └─ uses_service IControlledRepository<User> (Scoped (inferred))
        └─ method ReadQuery [L48]
          └─ implementation Cirrus.Data.Repository.Firm.UserRepository.ReadQuery
      └─ uses_service IControlledRepository<Office> (Scoped (inferred))
        └─ method ReadQuery [L46]
          └─ implementation Cirrus.Data.Repository.Firm.OfficeRepository.ReadQuery
  └─ returns DataverseAuditDto [L392]
  └─ impact_summary
    └─ requests 1
      └─ DataverseEntityAuditQuery
    └─ handlers 1
      └─ DataverseEntityAuditQueryHandler
    └─ mappings 1
      └─ DataverseAuditDto

