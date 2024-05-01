
using static Sparta_Dungeon.Define;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sparta_Dungeon
{
    public interface IDamagable
    {
        void Damage(float damage);
        void SkDamage(float damage);
    }
    public class Dungeon
    {

    }
}