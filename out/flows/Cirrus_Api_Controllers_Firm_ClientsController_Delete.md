[web] DELETE /api/clients/{id:Guid}  (Cirrus.Api.Controllers.Firm.ClientsController.Delete)  [L191–L197] status=200 [auth=user]
  └─ calls ClientRepository.WriteQuery [L194]
  └─ write Client [L194]
  └─ impact_summary
    └─ entities 1 (writes=1, reads=0)
      └─ Client writes=1 reads=0

