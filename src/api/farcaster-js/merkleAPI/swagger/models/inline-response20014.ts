/* tslint:disable */
/* eslint-disable */
/**
 * Merkle API
 * API documentation for all publicly exposed APIs provided by Merkle Manufactory, Inc for Farcaster V2.
 *
 * OpenAPI spec version: 2.0.0
 *
 *
 * NOTE: This class is auto generated by the swagger code generator program.
 * https://github.com/swagger-api/swagger-codegen.git
 * Do not edit the class manually.
 */
import { InlineResponse20014Result } from "./inline-response20014-result";
import { PaginationInfo } from "./PaginationInfo";
/**
 *
 * @export
 * @interface InlineResponse20014
 */
export interface InlineResponse20014 {
  /**
   *
   * @type {InlineResponse20014Result}
   * @memberof InlineResponse20014
   */
  result: InlineResponse20014Result;
  next?: PaginationInfo;
}