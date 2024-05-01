
using static Sparta_Dungeon.Define;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Sparta_Dungeon
{
    public interface IDamagable
    {
        void Damage(float Damage);
        void SkDamage(float SkDamage);
    }

    public class Dungeon
    {

    }
}
