module ElGamal

open System.Numerics
open System.Security.Cryptography

let g = BigInteger 666
let p = BigInteger 6661
let pk = BigInteger 2227
let r = BigInteger (RandomNumberGenerator.GetInt32(1, int p))
let m = BigInteger 2000
let c1 = BigInteger.ModPow(g, r, p)
let c2 = m * BigInteger.ModPow(pk, r, p)
// Begin Eve brute forcing Bob's private key
let bruteforce g p pk =
    let rec aux (i: int) =
       match BigInteger.ModPow(g, i, p) = pk with
        | true -> BigInteger i
        | false -> aux (i+1)
    aux 1
let bfsk = bruteforce g p pk // Eve now has Bob's private key, 66
let grsk = BigInteger.ModPow(c1, bfsk, p)
let decrypted = c2 / grsk // Eve now has the decrypted message, 2000
let malloryc2 = c2 * BigInteger 3 // Mallory doesn't have Bob's private key, but seeing that the message is simply an integer they can multiply it by 3 as 2000 * 3 = 6000
let bobMalloryDecrypt = malloryc2 / grsk // Bob decrypting Mallory's message, 6000