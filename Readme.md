COMO DEFINIR LA INFRAESTRUCTURA DE UN AGREGADO CON REPOSITORIO GENERICO

1. CREAR LA CLASE Y SE HEREDA DE ENTITY Y SE MARCAR COMO AGREGADO ROOT

namespace Banco.Domain
{
    public class Cliente:Entity<string>, IAggregateRoot
    {
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
    }
}

2. SE DEFINE EL CONTRATO DEL REPOSITORIO, se le indica que va a implementar el contrato del repositorio 
   generico, con la entidad de domino o agregado
 
    public interface ICuentaBancariaRepository:IGenericRepository<CuentaBancaria>
    {
    
    }

3. Implementar el repositorio generico para la entidad en la infraestructura, va a hererad del repositorio generico y va a implementar la interfaz que definimos para la entidad

namespace Banco.Infrastructure.Data
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(IDbContext context) : base(context)
        {
        }
    }
}
