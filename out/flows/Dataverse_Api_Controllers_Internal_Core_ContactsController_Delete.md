[web] DELETE /api/internal/contacts/{id}  (Dataverse.Api.Controllers.Internal.Core.ContactsController.Delete)  [L71–L79] status=200 [auth=Authentication.MachineToMachinePolicy,Authentication.RequireTenantIdPolicy]
  └─ calls ContactRepository (methods: Remove,WriteQuery) [L78]
  └─ delete Contact [L78]
    └─ reads_from DVS_Clients
  └─ write Contact [L74]
    └─ reads_from DVS_Clients
  └─ impact_summary
    └─ entities 1 (writes=2, reads=0)
      └─ Contact writes=2 reads=0

